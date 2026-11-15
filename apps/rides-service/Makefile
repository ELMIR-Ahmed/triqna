# Variables
PROJECT   := $(basename $(notdir $(firstword $(wildcard *.csproj))))
NAMESPACE := $(subst -,_,$(PROJECT))

.PHONY: help controller service dto entity repository mapper all-layers run watch

define check_name
@if [ -z "$(name)" ]; then echo "Erreur: Spécifiez le nom (ex: name=Product)"; exit 1; fi
endef

# Aide par défaut
help:
	@echo "Outils de génération pour Architecture N-Layers (Mapping manuel) :"
	@echo "  make controller name=X  -> Crée un API Controller dans /Controllers"
	@echo "  make service name=X     -> Crée une Interface et une Classe dans /Services"
	@echo "  make dto name=X         -> Crée un Record DTO dans /DTOs"
	@echo "  make entity name=X      -> Crée une Classe Entité dans /Entities"
	@echo "  make repository name=X  -> Crée une Interface et une Classe dans /Repositories"
	@echo "  make mapper name=X      -> Crée une classe de mapping manuel dans /Mappers"
	@echo "  make all-layers name=X  -> Génère TOUTES les couches d'un coup"
	@echo ""
	@echo "Namespace détecté : $(NAMESPACE)"

# 1. COUCHE PRÉSENTATION : API CONTROLLER
controller:
	$(check_name)
	@dotnet new apicontroller -n $(name)Controller -o Controllers
	@echo "✓ API Controller créé dans /Controllers"

# 2. COUCHE BUSINESS : SERVICES
service:
	$(check_name)
	@mkdir -p Services
	@printf '%s\n' \
		'namespace $(NAMESPACE).Services;' \
		'' \
		'public interface I$(name)Service' \
		'{' \
		'}' > Services/I$(name)Service.cs
	@printf '%s\n' \
		'namespace $(NAMESPACE).Services;' \
		'' \
		'public class $(name)Service : I$(name)Service' \
		'{' \
		'}' > Services/$(name)Service.cs
	@echo "✓ Interface et Classe Service créées dans /Services"

# 3. COUCHE DATA / COMMUNICATIONS : DTOs
dto:
	$(check_name)
	@mkdir -p DTOs
	@printf '%s\n' \
		'namespace $(NAMESPACE).DTOs;' \
		'' \
		'public record $(name)Dto' \
		'{' \
		'    public int Id { get; init; }' \
		'}' > DTOs/$(name)Dto.cs
	@echo "✓ Record DTO créé dans /DTOs"

# 4. COUCHE DOMAINE : ENTITIES
entity:
	$(check_name)
	@mkdir -p Entities
	@printf '%s\n' \
		'namespace $(NAMESPACE).Entities;' \
		'' \
		'public class $(name)' \
		'{' \
		'    public int Id { get; set; }' \
		'}' > Entities/$(name).cs
	@echo "✓ Classe Entité créée dans /Entities"

# 5. COUCHE ACCÈS DONNÉES : REPOSITORIES
repository:
	$(check_name)
	@mkdir -p Repositories
	@printf '%s\n' \
		'namespace $(NAMESPACE).Repositories;' \
		'' \
		'public interface I$(name)Repository' \
		'{' \
		'}' > Repositories/I$(name)Repository.cs
	@printf '%s\n' \
		'namespace $(NAMESPACE).Repositories;' \
		'' \
		'public class $(name)Repository : I$(name)Repository' \
		'{' \
		'}' > Repositories/$(name)Repository.cs
	@echo "✓ Interface et Classe Repository créées dans /Repositories"

# 6. COUCHE TRANSFORMATION : MAPPER MANUEL (méthodes d'extension statiques)
mapper:
	$(check_name)
	@mkdir -p Mappers
	@printf '%s\n' \
		'using $(NAMESPACE).Entities;' \
		'using $(NAMESPACE).DTOs;' \
		'' \
		'namespace $(NAMESPACE).Mappers;' \
		'' \
		'public static class $(name)Mapper' \
		'{' \
		'    public static $(name)Dto ToDto(this $(name) entity)' \
		'    {' \
		'        return new $(name)Dto' \
		'        {' \
		'            Id = entity.Id' \
		'        };' \
		'    }' \
		'' \
		'    public static $(name) ToEntity(this $(name)Dto dto)' \
		'    {' \
		'        return new $(name)' \
		'        {' \
		'            Id = dto.Id' \
		'        };' \
		'    }' \
		'}' > Mappers/$(name)Mapper.cs
	@echo "✓ Classe de mapping manuel créée dans /Mappers"

# GÉNÉRATION COMPLÈTE DE TOUTES LES COUCHES
all-layers:
	$(check_name)
	@$(MAKE) --no-print-directory entity name=$(name)
	@$(MAKE) --no-print-directory dto name=$(name)
	@$(MAKE) --no-print-directory repository name=$(name)
	@$(MAKE) --no-print-directory mapper name=$(name)
	@$(MAKE) --no-print-directory service name=$(name)
	@$(MAKE) --no-print-directory controller name=$(name)
	@echo "⚡ Architecture complète N-Layers générée pour '$(name)' !"

# RACCOURCIS UTILES
run:
	dotnet run

watch:
	dotnet watch run