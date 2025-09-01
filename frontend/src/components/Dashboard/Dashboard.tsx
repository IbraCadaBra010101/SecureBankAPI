import { useState, useEffect } from 'react';
import { Company } from '../../types/Company/Company';
import { apiService } from '../Common/apiService';
import ApartmentTable from '../Apartments/ApartmentTable';
import './Dashboard.css';

const Dashboard = () => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [selectedCompanyId, setSelectedCompanyId] = useState('');

  useEffect(() => {
    const loadCompanies = async () => {
      const data = await apiService.getCompanies();
      setCompanies(data);
      if (data.length > 0) {
        setSelectedCompanyId(data[0].companyId);
      }
    };
    loadCompanies();
  }, []);

  return (
    <div className="dashboard-container">
      <div className="dashboard-content">
        <div className="company-selector-card">
          <label className="company-selector-label">Company: </label>
          <select
            className="company-selector-select"
            value={selectedCompanyId}
            onChange={(e) => setSelectedCompanyId(e.target.value)}
          >
            {companies.map((company) => (
              <option key={company.companyId} value={company.companyId}>
                {company.name}
              </option>
            ))}
          </select>
        </div>
        <ApartmentTable companyId={selectedCompanyId} />
      </div>
    </div>
  );
};

export default Dashboard;
