/*
 * Copyright (c) 2013 christianr.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the GNU Lesser Public License v3
 * which accompanies this distribution, and is available at
 * http://www.gnu.org/licenses/lgpl-3.0.html
 * 
 * Contributors:
 *     christianr - initial API and implementation
 */

using System;
using UnityEngine;

namespace cc.creativecomputing.math.signal
{

	/// <summary>
	/// This is the base signal class that handles the basic setup, like scale and offset.
	/// Here you can also set how fractal values should be calculated. Values are always in the 
	/// range 0 to 1.
	/// @author christianriekoff
	/// 
	/// </summary>
	[Serializable]
	public abstract class CCSignal : CCSignalSettings
	{

		protected internal float _myOffsetX = 0;
		protected internal float _myOffsetY = 0;
		protected internal float _myOffsetZ = 0;

		protected internal CCSignalSettings _mySettings;

		public CCSignal(CCSignalSettings theSettings)
		{
			_mySettings = theSettings == null ? this : theSettings;
		}

		public CCSignal() : this(null)
		{
		}

		protected internal virtual void settings(CCSignalSettings theSettings)
		{
			_mySettings = theSettings;
		}

		/// <summary>
		/// Override this method to define how the 3d is calculated </summary>
		/// <param name="theX"> </param>
		/// <param name="theY"> </param>
		/// <param name="theZ"> </param>
		/// <returns> the calculated value </returns>
		public abstract float[] signalImpl(float theX, float theY, float theZ);
        
		public virtual float[] signalImpl(float theX, float theY)
		{
			return signalImpl(theX,theY,0);
		}
        
		public virtual float[] signalImpl(float theX)
		{
			return signalImpl(theX,0);
		}

		/// <summary>
		/// Returns multiple values for 3d coordinates, this is useful to get derivatives or 
		/// multiple output values like in case of the worley noise </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <returns> multiple values </returns>
		public float[] values(float theX)
		{
			float myScale = _mySettings.scale;
			float myFallOff = _mySettings.gain;

			float myOctaves = Mathf.Floor(_mySettings.octaves);
			float[] myResult = null;
			float myAmp = 0;

			for (int i = 0; i < myOctaves;i++)
			{
				float[] myValues = signalImpl(theX * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff;
				}
				myAmp += myFallOff;
				myFallOff *= _mySettings.gain;
				myScale *= _mySettings.lacunarity;
			}
			float myBlend = _mySettings.octaves - myOctaves;
			if (myBlend > 0)
			{
				float[] myValues = signalImpl(theX * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff * myBlend;
				}
				myAmp += myFallOff * myBlend;
			}
			if (myAmp > 0)
			{
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] /= myAmp;
				}
			}
			return myResult;
		}

		/// <summary>
		/// Returns the value for the given coordinates. By default only one band is calculated. </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <returns> the value for the given coordinates. </returns>
		public virtual float value(float theX)
		{
			return values(theX)[0];
		}

		/// <summary>
		/// Returns multiple values for 3d coordinates, this is useful to get derivatives or 
		/// multiple output values like in case of the worley noise </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <param name="theY"> y coord for the noise </param>
		/// <returns> multiple values </returns>
		public float[] values(float theX, float theY)
		{
			float myScale = _mySettings.scale;
			float myFallOff = _mySettings.gain;

			int myOctaves = (int)Mathf.Floor(_mySettings.octaves);
			float[] myResult = null;
			float myAmp = 0;

			for (int i = 0; i < myOctaves;i++)
			{
				float[] myValues = signalImpl(theX * myScale, theY * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff;
				}
				myAmp += myFallOff;
				myFallOff *= _mySettings.gain;
				myScale *= _mySettings.lacunarity;
			}
			float myBlend = _mySettings.octaves - myOctaves;
			if (myBlend > 0)
			{
				float[] myValues = signalImpl(theX * myScale, theY * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff * myBlend;
				}
				myAmp += myFallOff * myBlend;
			}
			if (myAmp > 0)
			{
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] /= myAmp;
				}
			}
			return myResult;
		}

		/// <summary>
		/// Returns multiple values for 3d coordinates, this is useful to get derivatives or 
		/// multiple output values like in case of the worley noise </summary>
		/// <param name="theVector"> coordinates for the noise </param>
		/// <returns> multiple values </returns>
		public virtual float[] values(Vector2 theVector)
		{
			return values(theVector.x, theVector.y);
		}

		/// <summary>
		/// Returns the value for the given coordinates. By default only one band is calculated. </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <param name="theY"> y coord for the noise </param>
		/// <returns> the value for the given coordinates. </returns>
		public virtual float value(float theX, float theY)
		{
			return values(theX, theY)[0];
		}

		/// <summary>
		/// Returns the value for the given coordinates. By default only one band is calculated. </summary>
		/// <param name="theVector"> coordinates for the noise </param>
		/// <returns> the value for the given coordinates. </returns>
		public virtual float value(Vector2 theVector)
		{
			return values(theVector)[0];
		}

		/// <summary>
		/// Returns multiple values for 3d coordinates, this is useful to get derivatives or 
		/// multiple output values like in case of the worley noise </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <param name="theY"> y coord for the noise </param>
		/// <param name="theZ"> z coord for the noise </param>
		/// <returns> multiple values </returns>
		public float[] values(float theX, float theY, float theZ)
		{
			float myScale = _mySettings.scale;
			float myFallOff = _mySettings.gain;

			int myOctaves = (int)Mathf.Floor(_mySettings.octaves);
			float[] myResult = null;
			float myAmp = 0;

			for (int i = 0; i < myOctaves;i++)
			{
				float[] myValues = signalImpl(theX * myScale, theY * myScale, theZ * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff;
				}
				myAmp += myFallOff;
				myFallOff *= _mySettings.gain;
				myScale *= _mySettings.lacunarity;
			}
			float myBlend = _mySettings.octaves - myOctaves;
			if (myBlend > 0)
			{
				float[] myValues = signalImpl(theX * myScale, theY * myScale, theZ * myScale);
				if (myResult == null)
				{
					myResult = new float[myValues.Length];
				}
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] += myValues[j] * myFallOff * myBlend;
				}
				myAmp += myFallOff * myBlend;
			}
			if (myAmp > 0)
			{
				for (int j = 0; j < myResult.Length;j++)
				{
					myResult[j] /= myAmp;
				}
			}
			return myResult;
		}

		/// <summary>
		/// Returns multiple values for 3d coordinates, this is useful to get derivatives or 
		/// multiple output values like in case of the worley noise </summary>
		/// <param name="theVector"> coordinates for the noise </param>
		/// <returns> multiple values </returns>
		public virtual float[] values(Vector3 theVector)
		{
			return values(theVector.x, theVector.y, theVector.z);
		}

		/// <summary>
		/// Returns the value for the given coordinates. By default only one band is calculated. </summary>
		/// <param name="theX"> x coord for the noise </param>
		/// <param name="theY"> y coord for the noise </param>
		/// <param name="theZ"> z coord for the noise </param>
		/// <returns> the value for the given coordinates. </returns>
		public virtual float value(float theX, float theY, float theZ)
		{
			return values(theX, theY, theZ)[0];
		}

		/// <summary>
		/// Returns the value for the given coordinates. By default only one band is calculated. </summary>
		/// <param name="theVector"> coordinates for the noise </param>
		/// <returns> the value for the given coordinates. </returns>
		public virtual float value(Vector3 theVector)
		{
			return values(theVector)[0];
		}
        
		public virtual float[] noiseImpl(float theX)
		{
			return signalImpl(theX,0);
		}

		public virtual void offset(float theX, float theY, float theZ)
		{
			_myOffsetX = theX;
			_myOffsetY = theY;
			_myOffsetZ = theZ;
		}
	}

}