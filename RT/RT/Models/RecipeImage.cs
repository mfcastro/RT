using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class RecipeImage
	{
		[Key]
		public int ImageID { get; set; }
		public int ImageSize { get; set; }
		public string FileName { get; set; }
		public byte[] ImageData { get; set; }
		//[Required(ErrorMessage = "Please select file")]
		//public HttpPostedFileBase File { get; set; }

		public string File
		{
			get
			{
				string mimeType = "image/png";
				string base64 = Convert.ToBase64String(ImageData);
				return string.Format("data:{0},{1}", mimeType, base64);
			}
		}
	}
}