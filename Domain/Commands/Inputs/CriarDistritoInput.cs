﻿using Domain.Contracts.Commands;
using FluentValidator;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CriarDistritoInput : Notifiable, ICommand
    {
        public string Numero { get; set; }
        public int Regiao { get; set; }
        public string Mascote { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public List<string> CodigoClubes { get; set; }
    }
}
