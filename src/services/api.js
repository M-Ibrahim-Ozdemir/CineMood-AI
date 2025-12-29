import axios from 'axios';

const BASE_URL = "https://localhost:7260/api"; //api base url

const TOKEN = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmOTY2ZTU0Ni0zODY1LTQ2ODUtYTZlNi04NjM3MjAyMGUxMzIiLCJ1bmlxdWVfbmFtZSI6Im1pYnJhaGltb3pkZW1pciIsIm5iZiI6MTc1NTkzOTM2MSwiZXhwIjoxNzU2NTQ0MTYxLCJpYXQiOjE3NTU5MzkzNjF9.2rZEUe2clBOh4tu1-XMXMcnlnmgQr8vxdBS1YUcK40k';

const axiosInstance = axios.create({
    baseURL: BASE_URL,
    headers: {
        'Authorization': `Bearer ${TOKEN}`,
        'Content-Type': 'application/json'
    }
});

//modu aldık
export const getRecommendation = async(mood) => {
    const response = await axiosInstance.post('/Recommendation', {mood});
    return response.data;
};