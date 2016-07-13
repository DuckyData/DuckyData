using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.ViewModels
{
    public static partial class ConfigureMaps
    {
        public static void ForMessage()
        {
            Mapper.CreateMap<Message, MessageAdd>();
            Mapper.CreateMap<Message, MessageBase>();
            Mapper.CreateMap<Message, MessageEdit>();
            Mapper.CreateMap<MessageAdd, Message>();
            Mapper.CreateMap<MessageAdd, Message>();
            Mapper.CreateMap<MessageAdd, MessageBase>();
            Mapper.CreateMap<MessageBase, Message>();
            Mapper.CreateMap<MessageBase, MessageEditForm>();
            Mapper.CreateMap<MessageEdit, MessageEditForm>();
            Mapper.CreateMap<Message, MessageAttachment>();
        }

    }

    

    public class MessageAttachment
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public string ContentName { get; set; }
        public byte[] Attachment { get; set; }
    }

    public class MessageList
    {
        public int Id { get; set; }
        public string Subject { get; set; }
    }

    public class MessageAddForm
    {

        [HiddenInput]
        public DateTime SentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
        
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

    }

    public class MessageAdd
    {

        [HiddenInput]
        public DateTime SentDate { get; set; }

        [HiddenInput]
        public string UserName { get; set; }

        public string UserId { get; set; }
        

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public bool viewed { get; set; }

        public HttpPostedFileBase Attachments { get; set; }


    }

    public class MessageBase : MessageAdd
    {
        public int Id { get; set; }
        
        public byte[] Attachment { get; set; }
        
        public string ContentType { get; set; }

        public string ContentName { get; set; }
        
        [Display(Name = "Attachment")]
        public string AttachUrl
        {
            get
            {
                return $"/image/{Id}";
            }
        }

    }

    

    public class MessageEdit
    {
        [HiddenInput]
        public int Id { get; set; }


        [HiddenInput]
        public string UserName { get; set; }

        [HiddenInput]
        public DateTime SentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }


        public bool viewed { get; set; }

        //attachment attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

    }

    public class MessageEditForm
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public DateTime SentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool viewed { get; set; }

        //attachment attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }
    }
}
