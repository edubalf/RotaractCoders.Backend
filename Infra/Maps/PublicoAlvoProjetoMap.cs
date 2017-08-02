﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Maps
{
    public class PublicoAlvoProjetoMap : EntityTypeConfiguration<PublicoAlvoProjeto>
    {
        public PublicoAlvoProjetoMap()
        {
            HasRequired(x => x.Projeto)
                .WithMany(x => x.PublicoAlvo)
                .HasForeignKey(x => x.IdProjeto);
        }
    }
}
