using CineMoodAI.Aplication.DTO;
using CineMoodAI.Aplication.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CineMoodAI.Infrastructure.Service
{
    public class OpenAIService : IAIService
    {
        private readonly HttpClient _httpClient;  //modli alan degiskenler
        private readonly string _apiKey;
        private readonly string _model;

        public OpenAIService(IConfiguration configuration) //configuration gidecek
        {
            _httpClient = new HttpClient(); //openaı servis yaratınca httpclient yaratcak
            _apiKey = configuration["OpenAI:ApiKey"] //configurationdan da openai altından keyyi alcak
                ?? throw new ArgumentNullException("OpenAI:ApiKey not found"); //yoksa hata verrce
            _model = configuration["OpenAI:Model"] ?? "gpt-4o";  //configuration dan openai altından modeli alcak, yoksa 4o olcak.  sonra alta prompt
        }



        public async Task<RecommendationResponse> GetRecommendationAsync(string mood)         ////kalanlar: AI service, autocantation, scaller.  web apide scaller yazak ekran gorek. sadecebu satır yukardakılerden ve asadakılard once                                                                                 //program cs ye git
        {
            var prompt = $"Sen sinema konusunda uzmanlaşmıs yapay zeka destekli bir film öneri asistanısın. " +
                $"'{mood}' ruh halindeki bir kullanıcıya 1 film öner. Yanıtı türkçe ver. " +
                $"Sadece şu şekilde bir JSON object formatında geri dçnüş yap: {{\"title\": \"filmin adı\", \"genre\": \"filmin türü\", \"description\": \"istenilen duygu durumuna neden olduğunu anlatan kısa açıklama\"    }}";

            var requestBody = new  //open aı e gondercegimiz request requestin bodysını ekledik. 
            {
                model = _model,  //model bu
                messages = new[] //mesajın icinde bir dizi
                {
                    new {role = "system", content = "Sen türkçe konuşan bir film öneri asistanısın"},  //role verdik her requestte bizi tanır. 
                    new {role = "user", content = prompt}
                },
                temperature = 1.0  //aynı cevap isterden az tutcan biz stemiyıoz yuksek tuttuk
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);  //serializer ettik body i  jeson requesti
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

          
            _httpClient.DefaultRequestHeaders.Authorization =    
                new AuthenticationHeaderValue("Bearer", _apiKey); //  //apikey gonderip eklicek

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content); //https clientle post yapcak bu adrese istekte bulunuyo
            response.EnsureSuccessStatusCode(); //ordan bi responxe doncek 200 oldugundan emin olcaz

            var ResponseBody = await response.Content.ReadAsStringAsync(); //sona bunu string olaarak okıyoz
            using var doc = JsonDocument.Parse(ResponseBody); //responsebyd ını json olarak parse edşuoz yanş json olarak isliyoz. onun cevabın icindeki kısımları alıyoz

            var messageContent = doc.RootElement   //onun cevabın icindeki kısımları alıyoz
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
            //sonra recomandatinu geri doncez
            //Jsonresponseye bınu serrilize et yani json responseye cevir neyi messagecontenti 
            var recommendation = JsonSerializer.Deserialize<RecommendationResponse>(messageContent!, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //recommnetaidon null degise recommnedationu return et nullsa recommendation yapıp bole bos seyler donuyo
            return recommendation ?? new RecommendationResponse 
            {
                Title = "File bulunamadı",
                Genre = "",
                Description = ""
            };
        }
    }
}
