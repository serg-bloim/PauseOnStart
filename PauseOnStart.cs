using System;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Console;
using Mafi.Core.Game;
using Mafi.Core.GameLoop;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Unity.InputControl;

namespace PauseOnStart;

public sealed class PauseOnStart : IMod
{
    // Mod constructor will be called on mod loading.
    public PauseOnStart(ModManifest manifest) : base()
    {
        Manifest = manifest;
        JsonConfig = new ModJsonConfig(this);
    }


    public void RegisterPrototypes(ProtoRegistrator registrator)
    {
    }

    public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
    {
    }

    public void EarlyInit(DependencyResolver resolver)
    {
    }

    public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
    {
        var gle = resolver.TryResolve<IGameLoopEvents>().ValueOrNull;
        var gsc = resolver.TryResolve<GameSpeedController>().ValueOrNull;
        if (gle != null && gsc != null)
        {
            void pauseGameOnStart()
            {
                gsc.RequestPause();
            }

            gle.RegisterNewGameInitialized(this, pauseGameOnStart);
        }
    }

    public void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
    {
    }

    public ModManifest Manifest { get; }
    public bool IsUiOnly { get; }
    public Option<IConfig> ModConfig { get; }
    public ModJsonConfig JsonConfig { get; }

    public void Dispose()
    {
    }
}