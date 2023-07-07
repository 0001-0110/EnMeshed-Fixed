using System;
using UnityEngine;

public abstract class ModularScreenController : ScreenController
{
	public enum ScreenMode
	{
		Menu,
		Loading,
		FailedConnection,
	}

	[Serializable]
	public class ScreenConfiguration
	{
		public ScreenMode ScreenMode;
		public GameObject[] gameObjects;
	}

	[SerializeField]
	private ScreenMode activeScreenMode;
	// NonReordable for diplay
	[SerializeField]
	[NonReorderable]
	private ScreenConfiguration[] screenModes;

	protected void SetMode(ScreenMode screenMode)
	{
		activeScreenMode = screenMode;
		// This code does not handle the case where a gameObject is in multiple configuration ate the same time
		foreach (ScreenConfiguration screenConfiguration in screenModes)
			foreach (GameObject gameObject in screenConfiguration.gameObjects)
				gameObject.SetActive(screenConfiguration.ScreenMode == activeScreenMode);
	}
}
