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
	/// @author christianriekoff
	/// 
	/// </summary>
	[Serializable]
	public class CCSinSignal : CCSignal
	{

		public CCSinSignal() : base()
		{
		}

		public CCSinSignal(CCSignalSettings theSettings) : base(theSettings)
		{
		}

		private float sin(float theValue)
		{
			float myResult = Mathf.Cos(theValue * 2 * Mathf.PI);
			if (_mySettings.normed)
			{
				myResult = (myResult + 1) / 2;
			}
			return myResult;
		}

		/* (non-Javadoc)
		 * @see cc.creativecomputing.math.signal.CCSignal#noiseImpl(float, float, float)
		 */
		public override float[] signalImpl(float theX, float theY, float theZ)
		{
			return new float[]{(sin(theX) * sin(theY) * sin(theZ))};
		}

		/* (non-Javadoc)
		 * @see cc.creativecomputing.math.signal.CCSignal#signalImpl(float, float)
		 */
		public override float[] signalImpl(float theX, float theY)
		{
			return new float[]{(sin(theX) * sin(theY))};
		}

		/* (non-Javadoc)
		 * @see cc.creativecomputing.math.signal.CCSignal#signalImpl(float)
		 */
		public override float[] signalImpl(float theX)
		{
			return new float[]{sin(theX)};
		}

	}

}