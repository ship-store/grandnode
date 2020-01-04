using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Grand.Core.Domain.Register
{
    public class Register : BaseEntity
    {

       
        [BsonElement]
        [Required(ErrorMessage = "Required Field")]
        public string Firstname { get; set; }
        [BsonElement]
        [Required(ErrorMessage = "Required Field")]
        public string Secondname { get; set; }
        [BsonElement]
        [Required(ErrorMessage = "Required Field")]
        public string Email { get; set; }
        [BsonElement]
        [Required(ErrorMessage = "Required Field")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Compare("Password", ErrorMessage = "Password Mis-Match")]
        public string ConfirmPassword { get; set; }
    }
}
