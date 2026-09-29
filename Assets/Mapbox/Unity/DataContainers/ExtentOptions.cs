namespace Mapbox.Unity.Map
{
	using UnityEngine;

	public interface ITileProviderOptions
	{
	}

	public interface ICameraBoundsExtentOptions : ITileProviderOptions
	{
		void SetOptions();
	}

	[System.Serializable]
	public class ExtentOptions : ITileProviderOptions
	{
		public virtual void SetOptions(ExtentOptions extentOptions)
		{
		}
	}
}
