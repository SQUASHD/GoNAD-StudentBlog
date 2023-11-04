![Frontend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-frontend.yml/badge.svg)
![Frontend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-frontend.yml/badge.svg)

# Student Blog Frontend

Welcome to the part of the stack that puts the 'N' in 'GoNAD' – the NextJS frontend for my Student Blog.

## Table of Contents

- [Quick Start](#quick-start)
- [Features](#features)
- [Setting It Up](#setting-it-up)

## Quick Start

Follow these steps to set up the project:

1. Clone the repository

2. Navigate to the frontend directory:

```bash
cd GoNAD-StudentBlog/frontend
```

3. Copy `.env.example` to `.env` and configure your environment variables.

```bash
cp .env.example .env
# Edit .env with your specific variables
```

###  Using npm

1. Install the dependencies:

```bash
npm install
```

2. Start the development server:

```bash
npm run dev
```

### Using Docker

Make sure you have Docker installed on your machine and that you've set the `NEXT_PUBLIC_API_URL`. Public environment variables are part of the build process for NextJS applications

1. Build your container

```bash
docker build -t studentblog-frontend .
```

2. Run your container

```bash
docker run -p 3000:3000 studentblog-frontend
```

Now you should be able to access the application at `http://localhost:3000`.

*NOTE*: You won't see much if you haven't set up the Backend API.

## Features

- Shadcn's UI library
- Some simple JWT handling
- Protected routes / conditional rendering
- React server components
