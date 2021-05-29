﻿using System;
using System.Linq;
using UnityEngine;
using VisualPinball.Engine.Game;
using VisualPinball.Engine.VPT.Table;

namespace VisualPinball.Unity
{
	public interface IPatcher
	{
		void Set(FileTableContainer th, string filename);
		void ApplyPrePatches(IRenderable item);
		void ApplyPatches(IRenderable item, GameObject gameObject, GameObject tableGameObject);
	}

	public static class PatcherManager
	{
		public static IPatcher GetPatcher() {
			var t = typeof(IPatcher);
			var patchers = AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.StartsWith("VisualPinball."))
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsClass && t.IsAssignableFrom(x))
				.Select(x => (IPatcher) Activator.CreateInstance(x))
				.ToArray();

			return patchers.First();
		}
	}
}
