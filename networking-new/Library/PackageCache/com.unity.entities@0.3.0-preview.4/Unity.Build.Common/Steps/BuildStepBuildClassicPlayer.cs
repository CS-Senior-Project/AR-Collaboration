﻿using System;
using System.IO;
using Unity.Build.Internals;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Unity.Build.Common
{
    [BuildStep(description = k_Description, category = "Classic")]
    sealed class BuildStepBuildClassicPlayer : BuildStep
    {
        const string k_Description = "Build Player";
        const string k_BootstrapFilePath = "Assets/StreamingAssets/livelink-bootstrap.txt";

        TemporaryFileTracker m_TemporaryFileTracker;

        public override string Description => k_Description;

        public override Type[] RequiredComponents => new[]
        {
            typeof(ClassicBuildProfile),
            typeof(SceneList),
            typeof(GeneralSettings)
        };

        public override Type[] OptionalComponents => new[]
        {
            typeof(OutputBuildDirectory),
            typeof(InternalSourceBuildConfiguration)
        };

        public static bool Prepare(BuildContext context, BuildStep step, bool liveLink, TemporaryFileTracker tracker, out BuildStepResult failure, out BuildPlayerOptions buildPlayerOptions)
        {
            buildPlayerOptions = default;
            var profile = step.GetRequiredComponent<ClassicBuildProfile>(context);
            if (profile.Target <= 0)
            {
                failure = BuildStepResult.Failure(step, $"Invalid build target '{profile.Target.ToString()}'.");
                return false;
            }

            if (profile.Target != EditorUserBuildSettings.activeBuildTarget)
            {
                failure = BuildStepResult.Failure(step, $"{nameof(EditorUserBuildSettings.activeBuildTarget)} must be switched before {nameof(BuildStepBuildClassicPlayer)} step.");
                return false;
            }

            var scenesList = step.GetRequiredComponent<SceneList>(context).GetScenePathsForBuild();
            if (scenesList.Length == 0)
            {
                failure = BuildStepResult.Failure(step, "There are no scenes to build.");
                return false;
            }

            var outputPath = step.GetOutputBuildDirectory(context);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var productName = step.GetRequiredComponent<GeneralSettings>(context).ProductName;
            var extension = profile.GetExecutableExtension();
            var locationPathName = Path.Combine(outputPath, productName + extension);

            buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = scenesList,
                target = profile.Target,
                locationPathName = locationPathName,
                targetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(profile.Target),
            };

            buildPlayerOptions.options = BuildOptions.None;
            switch (profile.Configuration)
            {
                case BuildConfiguration.Debug:
                    buildPlayerOptions.options |= BuildOptions.AllowDebugging | BuildOptions.Development;
                    break;
                case BuildConfiguration.Develop:
                    buildPlayerOptions.options |= BuildOptions.Development;
                    break;
            }

            var sourceBuild = step.GetOptionalComponent<InternalSourceBuildConfiguration>(context);
            if (sourceBuild.Enabled)
            {
                buildPlayerOptions.options |= BuildOptions.InstallInBuildFolder;
            }

            if (liveLink)
            {
                File.WriteAllText(tracker.TrackFile(k_BootstrapFilePath), BuildContextInternals.GetBuildSettingsGUID(context));
            }
            else
            {
                // Make sure we didn't leak a bootstrap file from a previous crashed build
                tracker.EnsureFileDoesntExist(k_BootstrapFilePath);
            }

            failure = default;
            return true;
        }

        public static BuildStepResult ReturnBuildPlayerResult(BuildContext context, BuildStep step, BuildReport report)
        {
            var result = new BuildStepResult(step, report);
            if (result.Succeeded)
            {
                var artifact = context.GetOrCreateValue<BuildArtifactDesktop>();
                artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            }
            return result;
        }

        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            m_TemporaryFileTracker = new TemporaryFileTracker();
            if (!Prepare(context, this, false, m_TemporaryFileTracker, out var failure, out var options))
                return failure;

            var report = UnityEditor.BuildPipeline.BuildPlayer(options);
            return ReturnBuildPlayerResult(context, this, report);
        }

        public override BuildStepResult CleanupBuildStep(BuildContext context)
        {
            m_TemporaryFileTracker.Dispose();
            return Success();
        }
    }
}
