using Newtonsoft.Json;
using RaidCatalog.Logic.Dtos;
using RaidCatalog.Logic.WebServices.Models;
using System.IO;
using System.Net;
using System.Text;

namespace RaidCatalog.Logic.WebServices {
    public class ArtifactWebService {
        public async Task<WebResult<WebArtDto[]>> GetArtsAsync(int userId) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/GetArts?UserId={userId}");
                request.Method = "GET";
                request.ContentType = "application/json";
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        var json = await reader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<WebResult<WebArtDto[]>>(json);
                    }
                }
            } catch (Exception e) {
                return new WebResult<WebArtDto[]>() { Message = e.Message };
            }
        }

        public async Task<WebResult<WebArtDto>> UpdateArtAsync(int userId, WebArtDto dto) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/UpdateArt?UserId={userId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync())) {
                    writer.Write(JsonConvert.SerializeObject(dto));
                }
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult<WebArtDto>>(reader.ReadToEnd());
                    }
                }
            } catch (Exception e) {
                return new WebResult<WebArtDto>() { Message = e.Message };
            }
        }

        public async Task<WebResult<WebArtDto>> AddArtAsync(int userId, WebArtDto dto) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/AddArt?UserId={userId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync())) {
                    writer.Write(JsonConvert.SerializeObject(dto));
                }
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult<WebArtDto>>(reader.ReadToEnd());
                    }
                }
            } catch (Exception e) {
                return new WebResult<WebArtDto>() { Message = e.Message };
            }
        }

        public async Task<WebResult> DeleteArtAsync(int userId, int artId) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/DeleteArt?UserId={userId}&Id={artId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult>(reader.ReadToEnd());

                    }
                }
            } catch (Exception e) {
                return new WebResult<List<WebArtDto>>() { Message = e.Message };
            }
        }

        public async Task<WebResult> DeleteArts(int userId, List<int> artIds) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/DeleteArts?UserId={userId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync())) {
                    writer.Write(JsonConvert.SerializeObject(artIds.ToArray()));
                }
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult>(reader.ReadToEnd());
                    }
                }
            } catch (Exception e) {
                return new WebResult<List<WebArtDto>>() { Message = e.Message };
            }
        }
    }
}
