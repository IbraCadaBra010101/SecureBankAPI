import React, { useState, useEffect } from 'react';
import { Company, Apartment } from '../types/realEstate';

const Dashboard: React.FC = () => {
  // State variables
  const [companies, setCompanies] = useState<Company[]>([]);
  const [apartments, setApartments] = useState<Apartment[]>([]);
  const [expiringIds, setExpiringIds] = useState<Set<string>>(new Set());
  const [selectedCompanyId, setSelectedCompanyId] = useState<string>('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Helper function
  const fetchData = async (url: string) => {
    const response = await fetch(url);
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    return response.json();
  };

  // Load companies
  const loadCompanies = async () => {
    try {
      const data = await fetchData('/api/apartments/companies');
      setCompanies(data);
      if (data.length > 0) setSelectedCompanyId(data[0].companyId);
    } catch (ex) {
      setError(ex instanceof Error ? ex.message : 'An error occurred');
    }
  };

  // Load apartments
  const loadApartments = async (companyId: string) => {
    if (!companyId) return;
    
    setLoading(true);
    setError(null);
    
    try {
      const [allApartments, expiringApartments] = await Promise.allSettled([
        fetchData(`/api/apartments/companies/${companyId}/apartments`),
        fetchData(`/api/apartments/companies/${companyId}/contracts/expiring?months=3`)
      ]);

      setApartments(allApartments.status === 'fulfilled' ? allApartments.value : []);
      setExpiringIds(expiringApartments.status === 'fulfilled' 
        ? new Set(expiringApartments.value.map((a: Apartment) => a.apartmentId))
        : new Set());
    } catch (ex) {
      setError(ex instanceof Error ? ex.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { loadCompanies(); }, []);
  useEffect(() => { if (selectedCompanyId) loadApartments(selectedCompanyId); }, [selectedCompanyId]);

  return (
    <div style={{
      minHeight: '100vh',
      display: 'flex',
      alignItems: 'flex-start',
      justifyContent: 'center',
      padding: '2rem',
      paddingTop: '15vh'
    }}>
      <div style={{
        maxWidth: '1200px',
        width: '100%'
      }}>
        {/* Company selector */}
        <div style={{marginBottom: '2rem', padding: '1.5rem', border: '1px solid #dee2e6', borderRadius: '0.5rem', backgroundColor: '#f8f9fa'}}>
          <label style={{fontWeight: 'bold', fontSize: '1.1rem'}}>Company: </label>
          <select 
            style={{marginLeft: '1rem', minWidth: '250px', padding: '0.5rem', borderRadius: '0.25rem', border: '1px solid #ced4da'}}
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

        {/* Loading */}
        {loading && <div style={{textAlign: 'center', padding: '2rem', fontSize: '1.2rem'}}>Loading...</div>}

        {/* Table */}
        {!loading && (
          <div style={{border: '1px solid #dee2e6', borderRadius: '0.5rem', overflow: 'hidden'}}>
            <table style={{width: '100%', borderCollapse: 'collapse'}}>
              <thead style={{backgroundColor: '#f8f9fa'}}>
                <tr>
                  <th style={{padding: '1rem', textAlign: 'left', borderBottom: '1px solid #dee2e6', fontWeight: 'bold'}}>Address</th>
                  <th style={{padding: '1rem', textAlign: 'center', borderBottom: '1px solid #dee2e6', fontWeight: 'bold'}}>Rooms</th>
                  <th style={{padding: '1rem', textAlign: 'right', borderBottom: '1px solid #dee2e6', fontWeight: 'bold'}}>Rent</th>
                  <th style={{padding: '1rem', textAlign: 'center', borderBottom: '1px solid #dee2e6', fontWeight: 'bold'}}>Status</th>
                </tr>
              </thead>
              <tbody>
                {apartments.map((apartment) => (
                  <tr key={apartment.apartmentId} style={{borderBottom: '1px solid #dee2e6'}}>
                    <td style={{padding: '1rem'}}>{apartment.address}</td>
                    <td style={{padding: '1rem', textAlign: 'center'}}>{apartment.rooms}</td>
                    <td style={{padding: '1rem', textAlign: 'right'}}>{apartment.rentPerMonth.toLocaleString()} SEK</td>
                    <td style={{padding: '1rem', textAlign: 'center'}}>
                      <span style={{
                        padding: '0.25rem 0.75rem',
                        borderRadius: '0.25rem',
                        fontSize: '0.875rem',
                        fontWeight: '500',
                        backgroundColor: expiringIds.has(apartment.apartmentId) ? '#dc3545' : '#198754',
                        color: 'white'
                      }}>
                        {expiringIds.has(apartment.apartmentId) ? 'Expiring' : 'Active'}
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        {/* Error */}
        {error && <div style={{marginTop: '2rem', padding: '1rem', backgroundColor: '#f8d7da', border: '1px solid #f5c6cb', borderRadius: '0.5rem', color: '#721c24'}}>{error}</div>}
      </div>
    </div>
  );
};

export default Dashboard;
