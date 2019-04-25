﻿using UnityEngine;
using System;

namespace cc.creativecomputing.math.signal
{
	
	[Serializable]
	public class CCLinearRandom : CCSimplexNoise
	{

		[Range(0, 1)]
		public float _cStepSize = 0.1f;

		public override float[] signalImpl(float theX)
		{

			float myDiv = theX / _cStepSize;
			float myLowerStep = Mathf.Floor(myDiv) * _cStepSize;
			float myUpperStep = myLowerStep + _cStepSize;
			float myBlend = (theX - myLowerStep) / _cStepSize;
			//CCLog.info(myBlend);


			float[] myLowerValues = base.signalImpl(myLowerStep);
			float[] myUpperValues = base.signalImpl(myUpperStep);

			float[] myResult = new float[myLowerValues.Length];

			for (int i = 0; i < myResult.Length;i++)
			{
				myResult[i] = Mathf.Lerp(myLowerValues[i], myUpperValues[i], myBlend);
			}

			return myResult;
		}

		public override float[] signalImpl(float theX, float theY)
		{
			// TODO Auto-generated method stub
			return base.signalImpl(theX, theY);
		}

		public override float[] signalImpl(float theX, float theY, float theZ)
		{
			// TODO Auto-generated method stub
			return base.signalImpl(theX, theY, theZ);
		}
	}

}