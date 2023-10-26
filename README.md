# GoNAD-StudentBlog

> **WARNING** 
> This is still a work in progress.
> While the backend is mostly finished, I'm still working on making a... good enough frontend.
> Docker and CI/CD pipeline to come.

Welcome to `GoNAD-StudentBlog` - the monorepo that just doesn't make sense. Concocted in a fever dream when I got a school assignment to make an asp.net core backend with basic authorization but I wanted to go beyond the requirements. 

## Overview

`GoNAD-StudentBlog`: a testament to what happens when hardheadedness meets an eagerness to learn. I could have gone the full MVC route with ASP.NET but I wanted to experience first-hand the difficulties in making a frontend for an extant API.

- `cli-utils`: Some [command-line tools made in GO](./cli-utils/) to ease my self-inflicted pain. Translate DTOs to TS types and check for console.logs (oops!)
- `frontend`: A [NextJS frontend](./frontend/), leveraging RSC to make a seamless user experience.
- `backend`: A fully fledged N-layer architecture [REST API](./backend/) for a simple CRUD application.

And why stop there? Why shouldn't I make it run in docker containers with a full CI/CD pipeline?
TODO: actually get docker and ci/cd pipeline up

## Getting Started

TODO:
