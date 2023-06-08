using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Helpers;

namespace GezginRobotProjesi {
    public class Maze {
        /// <summary>
        /// Verilen URL'deki metni okur ve 2d blok array'ine çevirir.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Response<List<List<Block>>>> CreateMap(string url) {
            Response<List<List<Block>>> map = new Response<List<List<Block>>>();
            Http client = new Http(url);
            Response<string> mapDataResponse = await client.Get();
            if(!mapDataResponse.IsSuccess){
                map.IsSuccess = false;
                map.ErrorMessage = mapDataResponse.ErrorMessage;
                return map;
            }
            map.Result = MazeHelper.SetMap(mapDataResponse.Result);
            map.IsSuccess = map.Result.Count > 0;
            map.ErrorMessage = !map.IsSuccess ? "Bilinmeyen hata oluştu!" : string.Empty;
            return map;
        }

        /// <summary>
        /// Genişlik ve Uzunluk parametrelerini alarak rastgele labirent oluşturur.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Response<List<List<Block>>> CreateMap(int height, int width) {
            Response<List<List<Block>>> map = new Response<List<List<Block>>>();
            if(width < Constant.MinimumSize.Width || width > Constant.MaximumSize.Width || height < Constant.MinimumSize.Height || height > Constant.MaximumSize.Height ) {
                map.IsSuccess = false;
                map.ErrorMessage = string.Format("Haritanın Genişliği veya Yüksekliği {0}-{1} arasında olmadılıdır! Verilen Genişlik: {2}, Verilen Yükseklik {3}", Constant.MinimumSize.Width, Constant.MaximumSize.Width, width, height);
                return map;
            }
            map.IsSuccess = true;
            map.Result = LabyrinthHelper.SetMap(height, width);
            return map;
        }
    }
}