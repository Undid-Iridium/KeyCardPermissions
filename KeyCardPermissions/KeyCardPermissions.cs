﻿using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using KeyCardPermissions.Handlers;
using System;

namespace KeyCardPermissions
{
    public class KeyCardPermissions : Plugin<Config>
    {


        public static Config early_config;

        /// <summary>
        /// Medium priority, lower prioritys mean faster loadin
        /// </summary>
        public override PluginPriority Priority { get; } = PluginPriority.Medium;



        private Harmony harmony;
        private string harmony_id = "com.Undid-Iridium.KeyCardPermissions";

        public override Version RequiredExiledVersion { get; } = new Version(4, 2, 0);


        /// <summary>
        /// Entrance function called through Exile
        /// </summary>
        public override void OnEnabled()
        {
            RegisterEvents();
            harmony = new Harmony(harmony_id);
            harmony.PatchAll();
            base.OnEnabled();

        }
        /// <summary>
        /// Destruction function called through Exile
        /// </summary>
        public override void OnDisabled()
        {
            UnRegisterEvents();
            harmony.UnpatchAll(harmony.Id);
            harmony = null;
            base.OnDisabled();
        }


        /// <inheritdoc cref="EventsHandler"/>
        public EventsHandler Handler { get; private set; }

        /// <summary>
        /// Registers events for EXILE to hook unto with cororotines (I think?)
        /// </summary>
        public void RegisterEvents()
        {
            // Register the event handler class. And add the event,
            // to the EXILED_Events event listener so we get the event.
            if (!Config.IsEnabled)
            {
                return;
            }
            early_config = Config;

            Handler = new EventsHandler(Config);
            Handler.Start();

            Log.Info("KeyCardPermissions has been loaded");

        }
        /// <summary>
        /// Unregisters the events defined in RegisterEvents, recommended that everything created be destroyed if not reused in some way.
        /// </summary>
        public void UnRegisterEvents()
        {
            Log.Info("KeyCardPermissions has been unloaded");
        }
    }
}
