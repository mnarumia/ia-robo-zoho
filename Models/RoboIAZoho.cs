using System;
using System.Collections.Generic;

namespace RoboIAZoho.Models
{
    public class TaskItem
    {
        public long Id { get; set; } // Usando long para o ID da tarefa do Zoho
        public string Name { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public long ProjectId { get; set; }

        public ICollection<SubTask> SubTasks { get; set; }
        public ICollection<TaskAttachment> Attachments { get; set; }
    }

    public class SubTask
    {
        public long Id { get; set; } // Usando long para o ID da subtarefa do Zoho
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }

        public long ParentTaskId { get; set; }
        public TaskItem ParentTask { get; set; }
    }

    public class TaskAttachment
    {
        public long Id { get; set; } // Usando long para o ID do anexo do Zoho
        public string FileName { get; set; }
        public string ZohoDownloadUrl { get; set; }
        public byte[] FileContent { get; set; } // Armazena o conteúdo do arquivo

        public long TaskId { get; set; }
        public TaskItem Task { get; set; }
    }
}