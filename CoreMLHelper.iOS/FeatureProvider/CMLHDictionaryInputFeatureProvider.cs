using System;
using CoreML;
using Foundation;

namespace CoreMLHelper.iOS
{
    /// <summary>
    /// CMLHD ictionary input feature provider.
    /// </summary>
    public class CMLHDictionaryInputFeatureProvider : NSObject, IMLFeatureProvider
    {
        private NSDictionary<NSObject, NSNumber> _inputDictionary { get; set; }

        public NSSet<NSString> FeatureNames => new NSSet<NSString>(new NSString("input"));

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreMLHelper.iOS.CMLHDictionaryInputFeatureProvider"/> class.
		/// </summary>
		/// <param name="InputDictionary">Input dictionary.</param>
		public CMLHDictionaryInputFeatureProvider(NSDictionary<NSObject, NSNumber> InputDictionary)
		{
            _inputDictionary = InputDictionary;
		}

        /// <summary>
        /// Gets the feature value.
        /// </summary>
        /// <returns>The feature value.</returns>
        /// <param name="featureName">Feature name.</param>
        public MLFeatureValue GetFeatureValue(string featureName)
        {
			if (featureName == "input")
			{
				var featureValue = MLFeatureValue.FromDictionary(_inputDictionary, out var error);

                if(error != null)
                {
                    throw new NSErrorException(error);
                }

				return featureValue;
			}

            throw new CMLHFeatureNotSupportedException();
		}
    }
}
