import React, { useState, useEffect, useCallback } from 'react';
import { Company, Apartment } from '../types/realEstate';

const Dashboard: React.FC = () => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [apartments, setApartments] = useState<Apartment[]>([]);
  const [expiringApartmentIds, setExpiringApartmentIds] = useState<Set<string>>(new Set());
  const [selectedCompanyId, setSelectedCompanyId] = useState<string>('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadCompanies = async () => {
    setError(null);
    try {
      const response = await fetch('/api/RealEstate/companies');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();
      setCompanies(data);
    } catch (ex) {
      setError(ex instanceof Error ? ex.message : 'An error occurred');
    }
  };

  const loadApartments = useCallback(async () => {
    if (!selectedCompanyId) return;
    
    setIsLoading(true);
    setError(null);
    try {
      const [allResponse, expiringResponse] = await Promise.all([
        fetch(`/api/RealEstate/companies/${selectedCompanyId}/apartments`),
        fetch(`/api/RealEstate/companies/${selectedCompanyId}/contracts/expiring?months=3`)
      ]);

      if (!allResponse.ok || !expiringResponse.ok) {
        throw new Error('Failed to fetch apartments data');
      }

      const allApartments = await allResponse.json();
      const expiringApartments = await expiringResponse.json();

      setApartments(allApartments);
      setExpiringApartmentIds(new Set(expiringApartments.map((a: Apartment) => a.apartmentId)));
    } catch (ex) {
      setError(ex instanceof Error ? ex.message : 'An error occurred');
    } finally {
      setIsLoading(false);
    }
  }, [selectedCompanyId]);

  useEffect(() => {
    loadCompanies();
  }, []);

  useEffect(() => {
    if (companies.length > 0 && !selectedCompanyId) {
      setSelectedCompanyId(companies[0].companyId);
    }
  }, [companies, selectedCompanyId]);

  useEffect(() => {
    if (selectedCompanyId) {
      loadApartments();
    }
  }, [selectedCompanyId, loadApartments]);

  const handleCompanyChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const companyId = event.target.value;
    setSelectedCompanyId(companyId);
  };

  return (
    <div>
      <div className="card mb-3">
        <div className="card-body">
          <div className="row g-3 align-items-center">
            <div className="col-auto">
              <label className="col-form-label">Company</label>
            </div>
            <div className="col-auto">
              <select 
                className="form-select" 
                style={{ minWidth: '260px' }}
                value={selectedCompanyId}
                onChange={handleCompanyChange}
              >
                {companies.map((company) => (
                  <option key={company.companyId} value={company.companyId}>
                    {company.name}
                  </option>
                ))}
              </select>
            </div>
          </div>
        </div>
      </div>

      {isLoading && (
        <div className="d-flex align-items-center">
          <div className="spinner-border spinner-border-sm me-2" role="status"></div>
          Loading...
        </div>
      )}

      {!isLoading && (
        <div className="card">
          <div className="card-body p-0">
            <div className="table-responsive">
              <table className="table table-hover table-sm align-middle m-0">
                <thead className="table-light">
                  <tr>
                    <th scope="col">Address</th>
                    <th scope="col" className="text-center">Rooms</th>
                    <th scope="col" className="text-end">Rent / mo</th>
                    <th scope="col" className="text-center">Status</th>
                  </tr>
                </thead>
                <tbody>
                  {apartments.map((apartment) => {
                    const isExpiring = expiringApartmentIds.has(apartment.apartmentId);
                    return (
                      <tr key={apartment.apartmentId}>
                        <td>{apartment.address}</td>
                        <td className="text-center">{apartment.rooms}</td>
                        <td className="text-end">{apartment.rentPerMonth.toLocaleString()} SEK</td>
                        <td className="text-center">
                          {isExpiring ? (
                            <span className="badge bg-danger">Expiring ≤ 3 months</span>
                          ) : (
                            <span className="badge bg-success">Active</span>
                          )}
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      )}

      {error && (
        <div className="alert alert-danger mt-3" role="alert">
          {error}
        </div>
      )}
    </div>
  );
};

export default Dashboard;
