using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace Fairhaven.Database
{
    public class Context: DbContext
    {
        public Context()
        {
            System.Data.Entity.Database.SetInitializer<Context>(new DataInitializer());
        }

        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponTemplate> WeaponTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeaponTemplate>()
            .HasMany<Modifier>(w => w.Modifiers)
            .WithMany();

            modelBuilder.Entity<Weapon>()
            .HasMany<Modifier>(w => w.Modifiers)
            .WithMany();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }

    public class DataInitializer : MigrateDatabaseToLatestVersion<Context, MigrationConfig> { }
    public class MigrationConfig : DbMigrationsConfiguration<Context>
    {
        public MigrationConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {
            #region modifiers - dmg
            context.Modifiers.AddOrUpdate(
              p => p.Id,
              new Modifier { Id = 3, ModifierValue = 5, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 4, ModifierValue = 8, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 5, ModifierValue = 10, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 6, ModifierValue = 12, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 7, ModifierValue = 22, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 8, ModifierValue = 41, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 9, ModifierValue = 98, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 10, ModifierValue = 105, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 11, ModifierValue = 112, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 12, ModifierValue = 123, Stat = Stat.Dmg, DamageType = DamageType.Physical, Rarity = Rarity.Casual }
            );
            #endregion

            #region modifiers - luk
            context.Modifiers.AddOrUpdate(
              p => p.Id,
              new Modifier { Id = 103, ModifierValue = 3, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 104, ModifierValue = 4, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 105, ModifierValue = 5, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 106, ModifierValue = 6, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 107, ModifierValue = 7, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 108, ModifierValue = 8, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 109, ModifierValue = 9, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 110, ModifierValue = 10, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 111, ModifierValue = 11, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 112, ModifierValue = 12, Stat = Stat.Luk, DamageType = DamageType.Physical, Rarity = Rarity.Casual }
            );
            #endregion

            #region modifiers - luk
            context.Modifiers.AddOrUpdate(
              p => p.Id,
              new Modifier { Id = 203, ModifierValue = 3, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 204, ModifierValue = 4, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 205, ModifierValue = 5, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 206, ModifierValue = 6, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 207, ModifierValue = 7, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 208, ModifierValue = 8, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 209, ModifierValue = 9, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 210, ModifierValue = 10, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 211, ModifierValue = 11, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual },
              new Modifier { Id = 212, ModifierValue = 12, Stat = Stat.Str, DamageType = DamageType.Physical, Rarity = Rarity.Casual }
            );
            #endregion

            context.SaveChanges();

            #region weapon templates - dmg

            context.WeaponTemplates.AddOrUpdate(
              p => p.Id,

              new WeaponTemplate { Id = 1, Level = 1, Name = "Whip", Rarity = Rarity.Casual, PrimaryModId = 3 },
              new WeaponTemplate { Id = 2, Level = 2, Name = "Sling", Rarity = Rarity.Casual, PrimaryModId = 4 },
              new WeaponTemplate { Id = 3, Level = 3, Name = "Dagger", Rarity = Rarity.Casual, PrimaryModId = 4 },
              new WeaponTemplate { Id = 4, Level = 4, Name = "Staff", Rarity = Rarity.Common, PrimaryModId = 5 },
              new WeaponTemplate { Id = 5, Level = 5, Name = "Short Sword", Rarity = Rarity.Common, PrimaryModId = 5 },
              new WeaponTemplate { Id = 6, Level = 6, Name = "Short Bow", Rarity = Rarity.Common, PrimaryModId = 6 },
              new WeaponTemplate { Id = 7, Level = 10, Name = "Crossbow", Rarity = Rarity.Uncommon, PrimaryModId = 6 },
              new WeaponTemplate { Id = 8, Level = 19, Name = "Long Sword", Rarity = Rarity.Uncommon, PrimaryModId = 7 },
              new WeaponTemplate { Id = 9, Level = 42, Name = "Long Bow", Rarity = Rarity.Rare, PrimaryModId = 8 },
              new WeaponTemplate { Id = 10, Level = 81, Name = "Mace", Rarity = Rarity.Epic, PrimaryModId = 9 },
              new WeaponTemplate { Id = 11, Level = 100, Name = "Hammer", Rarity = Rarity.Legendary, PrimaryModId = 10 },
              new WeaponTemplate { Id = 12, Level = 100, Name = "Axe", Rarity = Rarity.Artifact, PrimaryModId = 12 }
            );
            #endregion

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
