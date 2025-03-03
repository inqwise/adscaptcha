using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common
{
    public class AdsCaptchaOperationResults : IEnumerable<AdsCaptchaOperationResult>
    {
        private readonly List<AdsCaptchaOperationResult> _results = new List<AdsCaptchaOperationResult>();

        public void Add(AdsCaptchaOperationResult result)
        {
            _results.Add(result);
        }

        public IEnumerator<AdsCaptchaOperationResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        public bool HasErrors
        {
            get { return _results.Any(r => r.HasError); }
        }

        public bool IsEmpty
        {
            get { return _results.Count == 0; }
        }

        public void Add(AdsCaptchaErrors error)
        {
            Add(AdsCaptchaOperationResult.ToError(error));
        }

        public JsonObject ToJson()
        {
            var jo = new JsonObject();
            if (_results.Any())
            {
                jo = _results.First().ToJson();

                if (_results.Count > 1)
                {
                    jo.Put("errors", new JsonArray(_results.Select(r => r.ToJson())));
                }
            }

            return jo;
        }
    }
}