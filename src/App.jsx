import React, { useState } from "react";
import RecommendationForm from './components/RecommendationForm';
import RecommendationResult from './components/RecommendationResult';
import LoadingSpinner from "./components/LoadingSpinner";
import './App.css';

function App() {
    const [recommendation, setRecommendation] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const [customMood, setCustomMood] = useState('');

    // sabit token
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmOTY2ZTU0Ni0zODY1LTQ2ODUtYTZlNi04NjM3MjAyMGUxMzIiLCJ1bmlxdWVfbmFtZSI6Im1pYnJhaGltb3pkZW1pciIsIm5iZiI6MTc1NTkzOTM2MSwiZXhwIjoxNzU2NTQ0MTYxLCJpYXQiOjE3NTU5MzkzNjF9.2rZEUe2clBOh4tu1-XMXMcnlnmgQr8vxdBS1YUcK40k";

    const handleMoodSelect = async (mood) => {
    setLoading(true);
    setError(null);
    setRecommendation(null);

    try {
        const response = await fetch("https://localhost:7260/api/Recommendation", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({mood})
         });

         if (!response.ok) {
            throw new  Error(`HTTP error! status: ${response.status}`);
         }

         const data = await response.json();
         setRecommendation(data);
        } catch(err) {
            console.error('Film tavsiyesi alınırken hata oluştu:', err);
            setError('Film tavsiyesi alınamadı. Lütfen daha sonra tekrar deneyin. ');
        } finally{
            setLoading(false);
        }
    };
    
    const handleCustomMoodSubmit = (e) => {
        e.preventDefault();
        if(customMood.trim()){
            handleMoodSelect(customMood);
        }
    };

    return(
        <div className="app-container">
          <div className="header text-center py-5 bg-dark text-white">
            <h1 className="display-4"> Film Tavsiye Sistemi</h1>
            <p className="lead"> Ruh halinize göre film önerileri alın</p>
        </div>

        <div className="container py-4">
          <RecommendationForm
            onMoodSelect={handleMoodSelect}
            customMood={customMood}
            setCustomMood={setCustomMood}
            onCustomMoodSubmit={handleCustomMoodSubmit}  //sebebini ogren
          />
        
          {loading ? (
            <LoadingSpinner />
          ): (
            <RecommendationResult recommendation = {recommendation} error={error} />
          )}
        </div>

        <footer className="bg-dark text-white text-center py-3">
          <p> 2025 Film Tavsiye Sistemi</p>
        </footer>
      </div>  
    );
}

export default App;
