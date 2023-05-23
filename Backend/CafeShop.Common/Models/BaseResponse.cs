using Newtonsoft.Json;

namespace CafeShop.Common.Models {
    public class BaseRes {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }

        public static BaseRes Ok() {
            return new() { Success = true };
        }

        public static BaseRes Fail(string? message = null) {
            return new() { Message = message };
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class BaseRes<T> : BaseRes {

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T? Data { get; set; }

        public static BaseRes<T> Ok(T? data) {
            return new() { Success = true, Data = data };
        }
    }

    public class BaseListData<T> {

        [JsonProperty("items")]
        public List<T> Items { get; set; } = new();

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class FileRes {
        public string FileName { get; set; } = string.Empty;
        public byte[] ByteArray { get; set; } = Array.Empty<byte>();
    }
}