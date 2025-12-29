import React from "react";

function LoadingSpinner(){
    return(
        <div className="loading-spinner d-flex flex-column align-align-items-center justify-content-center py-5">
            <div className="spinner-border text-primary" style={{width: '4rem', height:'4rm'}} role="status">
                <span className="visually-hidden"> Yükleniyor</span>
            </div>
            <h3 className="mt-4 text-primary"> Film Öneriniz Hazırlanıyor</h3>
            <p className="text-muted mt-2"> Ruh halinize en uygun film aranıyor...</p>

            <div className="mt-4 film-reel-animation">
                <i className="bi bi-film fs-1 text-secondary mx-2"></i>
                <i className="bi bi-film fs-1 text-secondary mx-2"></i>
                <i className="bi bi-film fs-1 text-secondary mx-2"></i>
            </div>
        </div>
    );
}

export default LoadingSpinner;