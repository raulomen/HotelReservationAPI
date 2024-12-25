# Nombre del Proyecto

Creación de una Appi para realizar reservas en un hotel

## Tecnologías Utilizadas

Este proyecto fue desarrollado utilizando las siguientes tecnologías:

- **Framework**: .NET 7
- **IDE**: Visual Studio 2022
- **Base de datos**: MongoDB

## Arquitectura del Proyecto

El proyecto sigue una **arquitectura basada en capas**, que está diseñada para separar las diferentes responsabilidades y mejorar la mantenibilidad y escalabilidad del sistema. Las capas principales del proyecto son:

1. Controllers: Contiene los controladores, que suelen ser responsables de manejar las solicitudes HTTP entrantes y retornar respuestas. En términos de arquitectura, esto pertenece a la capa de presentación o interfaz.
2. Core: Normalmente, esta carpeta incluye la lógica de negocio central (domain layer) y puede contener entidades, interfaces de servicios y posiblemente DTOs (Data Transfer Objects).
3. Infrastructure: Aquí se maneja la implementación de servicios, acceso a datos (por ejemplo, repositorios) o integración con otros sistemas externos. Pertenece a la capa de infraestructura.
4. Services: Se puede usar para implementar lógica de negocio que interactúe con la capa Core o Infrastructure. Es una forma de separar responsabilidades.

## Patrones de Diseño Utilizados

El proyecto hace uso de varios **patrones de diseño** para mejorar la eficiencia, la escalabilidad y la mantenibilidad del código. Los patrones implementados son:

1. Inyección de dependencias: la inyección de dependencias permite que los controladores y servicios no gestionen directamente sus dependencias, sino que estas se inyectan a través de los constructores.
2. Repositorio: esta capa es responsable de interactuar directamente con la base de datos, y el servicio
3. Servicio: clases que contienen la lógica de negocio de la aplicación. Estos servicios ejecuta las tareas específicas y utilizan los repositorios para obtener o almacenar datos
4. Fábrica: implícita a través de la inyección de dependencias.
5. Adaptador: para interactuar con sistemas externos.
6. Estrategia: en el caso de tener múltiples implementaciones de servicios como el correo.

## Instalación

### Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalados los siguientes programas:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) con la carga de trabajo de desarrollo de aplicaciones .NET.
- .NET 6 SDK
- Base de datos configurada en MongoDB, crear un cluster en Atlas.

