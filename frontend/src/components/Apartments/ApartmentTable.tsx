import { useState, useEffect } from 'react';
import { Apartment } from '../../types/Apartment/Apartment';
import { apiService } from '../Common/apiService';
import './ApartmentTable.css';

const ApartmentTable = ({ companyId }: { companyId: string }) => {
  const [apartments, setApartments] = useState<Apartment[]>([]);
  const [expiringIds, setExpiringIds] = useState<Set<string>>(new Set());
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (!companyId) return;
    
    const loadApartments = async () => {
      setLoading(true);
      const [apartmentsData, expiringData] = await Promise.all([
        apiService.getApartments(companyId),
        apiService.getExpiringContracts(companyId),
      ]);
      setApartments(apartmentsData);
      setExpiringIds(new Set(expiringData.map(a => a.apartmentId)));
      setLoading(false);
    };
    loadApartments();
  }, [companyId]);

  if (loading) {
    return (
      <div className="text-center mt-4">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    );
  }

  if (apartments.length === 0) {
    return null;
  }
  return (
    <div className="apartment-table-container">
      <table className="apartment-table">
        <thead className="apartment-table-header">
          <tr>
            <th className="apartment-table-th">Address</th>
            <th className="apartment-table-th">Rooms</th>
            <th className="apartment-table-th">Rent</th>
            <th className="apartment-table-th">Status</th>
          </tr>
        </thead>
        <tbody>
          {apartments.map((apartment) => (
            <tr key={apartment.apartmentId} className="apartment-table-row">
              <td className="apartment-table-td">{apartment.address}</td>
              <td className="apartment-table-td">{apartment.rooms}</td>
              <td className="apartment-table-td">{apartment.rentPerMonth.toLocaleString()} SEK</td>
              <td className="apartment-table-td">
                <span className={expiringIds.has(apartment.apartmentId) ? 'status-badge status-badge-expiring' : 'status-badge status-badge-active'}>
                  {expiringIds.has(apartment.apartmentId) ? 'Expiring' : 'Active'}
                </span>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ApartmentTable;
