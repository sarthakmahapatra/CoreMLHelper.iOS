using System;
using Foundation;

namespace CoreMLHelper.iOS
{
    public class CMLHDictionaryPrediction
    {
     
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CoreMLHelper.iOS.CMLHDictionaryPrediction"/> class.
        /// </summary>
        /// <param name="classLable">Class lable.</param>
        /// <param name="classProbability">Class probability.</param>
        public CMLHDictionaryPrediction(string classLable ,NSDictionary<NSObject, NSNumber> classProbability )
        {
            ClassLable = classLable;
            ClassProbability = classProbability;
        }

        public string ClassLable
        {
            get;
			private set;
   
        }

        public NSDictionary<NSObject, NSNumber> ClassProbability
        {
            get;
            private set;
        }
    }
}
