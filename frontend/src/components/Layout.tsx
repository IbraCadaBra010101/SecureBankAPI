import React, { ReactNode } from 'react';

interface LayoutProps {
  children: ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div className="page-wrapper">
      <header>
        <nav className="navbar navbar-expand-lg navbar-dark bg-brand">
          <div className="container">
            <div className="navbar-brand-section">
              <span className="navbar-brand">Real Estate Management</span>
            </div>
            <button 
              className="navbar-toggler" 
              type="button" 
              data-bs-toggle="collapse" 
              data-bs-target="#navbarNav" 
              aria-controls="navbarNav" 
              aria-expanded="false" 
              aria-label="Toggle navigation"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
                <li className="nav-item">
                  <a className="nav-link" href="/">Dashboard</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link" href="/swagger" target="_blank" rel="noopener noreferrer">
                    API Docs
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </header>

      <main className="container">
        {children}
      </main>
    </div>
  );
};

export default Layout;
