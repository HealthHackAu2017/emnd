using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

namespace Emnd
{
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(RefType) && property.PropertyName == "id")
            {
                property.ShouldSerialize = (obj) => false;
            }

            return property;
        }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }

    public class DateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //canconvert guarantees we get a datetime object
            DateTime date = (DateTime)value;
            writer.WriteValue(date.ToString("yyyy-MM-dd HH:mm"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime result = new DateTime();
            try
            {
                //"2017-10-03T11:44:22.741Z",
                result = DateTime.ParseExact(reader.Value.ToString(), new string[] { "yyyy-MM-dd H:mm", "yyyy-MM-dd H:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'" }, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DateTime Conversion Exception: " + ex.Message); 
                Debug.WriteLine("Stack trace: " + ex.StackTrace);
            }
            //FIXME Debug.WriteLine($"DateTime {reader.Value.ToString()} converted to {result}");
            return result;
        }
    }

    public static class JsonData
    {
        static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            Debug.WriteLine(currentError);
            errorArgs.ErrorContext.Handled = true;
        }
        static JsonSerializerSettings serializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            Converters = new List<JsonConverter> { new DateTimeJsonConverter() }
            //ContractResolver = new ShouldSerializeContractResolver()
        };
        static JsonSerializerSettings deserializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            //FIXME DateParseHandling = DateParseHandling.None,
            Converters = new List<JsonConverter> { new DateTimeJsonConverter() },
            Error = HandleDeserializationError
        };
        public static string AsJsonString(this object obj, bool indented = false) => JsonConvert.SerializeObject(obj, indented ? Formatting.Indented : Formatting.None, serializerSettings);
        public static T FromJsonString<T>(string jsonData) => JsonConvert.DeserializeObject<T>(jsonData, deserializerSettings);
    }

    //[PropertyChanged.ImplementPropertyChanged]
    public class ID
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        protected ID()
        {
            Id = "0";
        }
        protected ID(ID id)
        {
            Overwrite(id);
        }
        public void Overwrite(ID a)
        {
            Id = a.Id;
        }

        public static T ZeroHandler<T>(string Id) where T : ID, new()
        {
            return Id == "0" ? new T() : null;
        }
    }

    //[PropertyChanged.ImplementPropertyChanged]
    public class RefType : ID
    {
        [JsonIgnore]
        public static string Class = "ref";

        [JsonProperty("text")]
        protected string text;
        [JsonIgnore]
        virtual public string Text
        {
            get { return text; }
            set { text = value; }
        }


        public RefType()
        {
            text = "N/A";
        }
        public RefType(RefType a) : base(a)
        {
            Overwrite(a);
        }
        public void Overwrite(RefType a)
        {
            base.Overwrite(a);
            Text = a.Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class ListContainer<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        public ListContainer()
        {
            Data = new List<T>();
        }

        public static ListContainer<T> Deserialize(string jsonData)
        {
            return JsonConvert.DeserializeObject<ListContainer<T>>(jsonData, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
        }
    }

    public class JsonId : ListContainer<JsonId>
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        public new static long Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<JsonId>(json, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            }).Data.First().ID;
        }
    }

}