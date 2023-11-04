![Frontend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-frontend.yml/badge.svg)
![Frontend CD Preview](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-frontend-preview.yml/badge.svg)
![Frontend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-frontend-prod.yml/badge.svg)
# Student Blog Frontend

Welcome to the part of the stack that puts the 'N' in 'GoNAD' – the NextJS frontend for my Student Blog.

## My Approach

In a typical scenario, I'd have gone full-stack, enjoying the seamless harmony and typesafety of an all-in-one solution.
Having built the backend first, I wanted to experience the unique hurdles of tying a frontend to a pre-existing,
fully-formed backend. I've intentionally tried to limit the use of server actions, using them primarily to set and
refresh cookies. With the new experimental taint API it's recommended to use DTOs and a data access layer (go figure)
to safely handle sensitive data, but why would I repeat the work that I've already done with the backend?

## Features

- Shadcn's UI library
- Some simple JWT handling
- Protected routes / conditional rendering
- Minimal server actions.
- React server components

## Setting It Up

TODO
