![Frontend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-frontend.yml/badge.svg)
![Frontend CD Preview](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-frontend-preview.yml/badge.svg)
![Frontend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-frontend-pro.yml/badge.svg)
![Backend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-backend.yml/badge.svg)
![Backend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-backend.yml/badge.svg)

# GoNAD-StudentBlog

**Go** CLI Utilities, **N**extJS frontend,**A**SP.NET Core Backend with **D**ockerization. Or `GoNAD` for short.

Welcome to the `GoNAD-StudentBlog` - the monorepo that just doesn't make sense, with the stack no one asked for, leveraging only half the power of NextJS and ASP.NET Core.
Concocted in a fever dream when I got a school assignment to make an asp.net core backend with basic authorization
but I wanted to go beyond the requirements.

## Overview

This project is testament to what happens when hardheadedness meets an eagerness to learn. I could have gone the
full MVC route with ASP.NET but I wanted first-hand experience with the difficulties in making a frontend for an extant
API.

- `cli-utils`: Some [command-line tools made in GO](./cli-utils/) to ease my self-inflicted pain. Translate C# DTOs (Data Transfer Objects) to TypeScript types for type-safety in interfacing with the api and check for console.logs (oops!)
- `frontend`: A [NextJS frontend](./frontend/), leveraging RSC (React Server Components) to make a seamless user experience.
- `backend`: A fully fledged N-layer architecture [REST API](./backend/) for a simple CRUD application.

And why stop there? Why shouldn't I make it run in docker containers with a full CI/CD pipeline?
TODO: actually get docker and ci/cd pipeline up

## Getting Started

TODO:

### Prequisites

### Cloning the Repo

### Setting up

#### Backend

#### Frontend

#### Environment Variables
