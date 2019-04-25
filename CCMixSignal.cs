using UnityEngine.Serialization;

namespace cc.creativecomputing.math.signal
{
	using System;
	using UnityEngine;

	[Serializable]
	public class CCMixSignal : CCSignal
	{

		[Range(min : 0, max : 1)]
		public float saw = 0;
		[Range(min : 0, max : 1)]
		public float simplex = 0;
		[Range( min : 0, max : 1)]
		public float sine = 0;
		[Range(min : 0, max : 1)]
		public float square = 0;
		[Range(min : 0, max : 1)]
		public float tri = 0;
		[Range(min : 0, max : 1)]
		public float slopedTri = 0;
		[Range(min : 0, max : 1)]
		public float amp = 0;


		private CCSawSignal _mySaw;
		private CCSimplexNoise _mySimplex;
		private CCSinSignal _mySine;
		
		public CCSquareSignal squareSig;
		
		public CCTriSignal triSig;
		private CCSlopedTriSignal _mySlopedtriSig;

		public CCMixSignal()
		{
			_mySaw = new CCSawSignal(this);
			_mySimplex = new CCSimplexNoise(this);
			_mySine = new CCSinSignal(this);
			squareSig = new CCSquareSignal(this);
			triSig = new CCTriSignal(this);
			_mySlopedtriSig = new CCSlopedTriSignal(this);
		}

		private float mixSignal(float[] theSaw, float[] theSimplex, float[] theSine, float[] thesquareSig, float[] thetriSig, float[] theSlopedtriSig)
		{
			float myMaxAmount = saw + simplex + sine + square + tri + slopedTri;
			if (myMaxAmount == 0)
			{
				return 0;
			}
			return (
				theSaw[0] * saw + 
				theSimplex[0] * simplex + 
				theSine[0] * sine + 
				thesquareSig[0] * square + 
				thetriSig[0] * tri + 
				theSlopedtriSig[0] * slopedTri
			) / myMaxAmount * amp;
		}

		public override float[] signalImpl(float theX, float theY, float theZ)
		{
			return new float[]{mixSignal(
				saw == 0 ? new float[]{0} : _mySaw.signalImpl(theX, theY, theZ), 
				simplex == 0 ? new float[]{0} : _mySimplex.signalImpl(theX, theY, theZ), 
				sine == 0 ? new float[]{0} : _mySine.signalImpl(theX, theY, theZ), 
				square == 0 ? new float[]{0} : squareSig.signalImpl(theX, theY, theZ), 
				tri == 0 ? new float[]{0} : triSig.signalImpl(theX, theY, theZ), 
				slopedTri == 0 ? new float[]{0} : _mySlopedtriSig.signalImpl(theX, theY, theZ))
			};
		}

		public override float[] signalImpl(float theX, float theY)
		{
			return new float[]{mixSignal(
				saw == 0 ? new float[]{0} : _mySaw.signalImpl(theX, theY), 
				simplex == 0 ? new float[]{0} : _mySimplex.signalImpl(theX, theY), 
				sine == 0 ? new float[]{0} : _mySine.signalImpl(theX, theY), 
				square == 0 ? new float[]{0} : squareSig.signalImpl(theX, theY), 
				tri == 0 ? new float[]{0} : triSig.signalImpl(theX, theY), 
				slopedTri == 0 ? new float[]{0} : _mySlopedtriSig.signalImpl(theX, theY))
			};
		}

		public override float[] signalImpl(float theX)
		{
			return new float[]{mixSignal(
				saw == 0 ? new float[]{0} : _mySaw.signalImpl(theX), 
				simplex == 0 ? new float[]{0} : _mySimplex.signalImpl(theX), 
				sine == 0 ? new float[]{0} : _mySine.signalImpl(theX), 
				square == 0 ? new float[]{0} : squareSig.signalImpl(theX), 
				tri == 0 ? new float[]{0} : triSig.signalImpl(theX), 
				slopedTri == 0 ? new float[]{0} : _mySlopedtriSig.signalImpl(theX))
			};
		}

	}

}