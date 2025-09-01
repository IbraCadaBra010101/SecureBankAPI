import { Company } from '../../types/Company/Company';
import { Apartment } from '../../types/Apartment/Apartment';

export const apiService = {
  async getCompanies(): Promise<Company[]> {
    const response = await fetch('/api/apartments/companies');
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    return response.json();
  },

  async getApartments(companyId: string): Promise<Apartment[]> {
    const response = await fetch(`/api/apartments/companies/${companyId}/apartments`);
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    return response.json();
  },

  async getExpiringContracts(companyId: string): Promise<Apartment[]> {
    const response = await fetch(`/api/apartments/companies/${companyId}/contracts/expiring?months=3`);
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    return response.json();
  }
};
