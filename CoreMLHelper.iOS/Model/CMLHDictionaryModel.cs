using System;
using CoreML;
using Foundation;

namespace CoreMLHelper.iOS
{
    /// <summary>
    /// CMLH Dictionary model.
    /// </summary>
    public class CMLHDictionaryModel
    {
        private MLModel _model;
        private NSUrl _modelUrl;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreMLHelper.iOS.Model.CMLHDictionaryModel"/> class.
		/// </summary>
		/// <param name="modelName">Model name.</param>
		/// <param name="isCompiled">If set to <c>true</c> is compiled.</param>
		public CMLHDictionaryModel(string modelName, bool isCompiled)
        {
            NSBundle bundle = NSBundle.MainBundle;

            if (!isCompiled)
            {
                var assetPath = bundle.GetUrlForResource(modelName, "mlmodel");
                _modelUrl = MLModel.CompileModel(assetPath, out var error1);

                if (error1 != null) throw new NSErrorException(error1);
            }
            else
                _modelUrl = bundle.GetUrlForResource(modelName, "mlmodelc");

            _model = MLModel.FromUrl(_modelUrl, out var error2);

            if (error2 != null) throw new NSErrorException(error2);

        }

        /// <summary>
        /// Gets the prediction.
        /// </summary>
        /// <returns>The prediction.</returns>
        /// <param name="dictionaryInputFeatureProvider">Dictionary input feature provider.</param>
        public CMLHDictionaryPrediction GetPrediction(CMLHDictionaryInputFeatureProvider dictionaryInputFeatureProvider)
        {
            var predection = _model.GetPrediction(dictionaryInputFeatureProvider, out var error);

            if (error != null) throw new NSErrorException(error);

            return new CMLHDictionaryPrediction(predection.GetFeatureValue("classLabel").StringValue, predection.GetFeatureValue("classProbability").DictionaryValue);
        }

        /// <summary>
        /// Gets the prediction.
        /// </summary>
        /// <returns>The prediction.</returns>
        /// <param name="dictionaryInput">Dictionary input.</param>
		public CMLHDictionaryPrediction GetPrediction(NSDictionary<NSObject, NSNumber> dictionaryInput)
		{
            var dictionaryInputFeatureProvider = new CMLHDictionaryInputFeatureProvider(dictionaryInput);
			var predection = _model.GetPrediction(dictionaryInputFeatureProvider, out var error);

			if (error != null) throw new NSErrorException(error);

			return new CMLHDictionaryPrediction(predection.GetFeatureValue("classLabel").StringValue, predection.GetFeatureValue("classProbability").DictionaryValue);
		}

    }
}
