# Real Estate Frontend

This is a React TypeScript frontend for the Real Estate Management System.

## Features

- Company selection dropdown
- Apartment listing with status indicators
- Responsive design using Bootstrap
- TypeScript for type safety

## Getting Started

### Prerequisites

- Node.js (version 16 or higher)
- npm or yarn

### Installation

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the development server:
   ```bash
   npm start
   ```

3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

### Building for Production

```bash
npm run build
```

## Project Structure

```
src/
├── components/          # React components
│   ├── Layout.tsx      # Main layout with navigation
│   └── Dashboard.tsx   # Main dashboard component
├── types/              # TypeScript type definitions
│   └── realEstate.ts  # Real estate data types
├── App.tsx             # Main app component
├── index.tsx           # Entry point
└── index.css           # Global styles
```

## API Integration

The frontend communicates with the Real Estate API backend. Make sure the backend is running on `https://localhost:7001` or update the proxy configuration in `package.json` if needed.

## Available Scripts

- `npm start` - Runs the app in development mode
- `npm test` - Launches the test runner
- `npm run build` - Builds the app for production
- `npm run eject` - Ejects from Create React App (one-way operation)
