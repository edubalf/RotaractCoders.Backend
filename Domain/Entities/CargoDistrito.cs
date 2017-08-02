﻿using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class CargoDistrito : Entity
    {
        public Guid IdSocio { get; private set; }
        public Guid IdDistrito { get; private set; }
        public Guid IdCargo { get; private set; }
        public DateTime De { get; private set; }
        public DateTime Ate { get; private set; }
        public Socio Socio { get; private set; }
        public Distrito Distrito { get; private set; }
        public Cargo Cargo { get; private set; }

        protected CargoDistrito()
        {

        }

        public CargoDistrito(Guid idSocio, Guid idDistrito, Guid idCargo, DateTime de, DateTime ate)
        {
            IdSocio = idSocio;
            IdDistrito = idDistrito;
            IdCargo = idCargo;
            De = de;
            Ate = ate;
        }
    }
}
