import React from 'react';

function RecommendationResult({ recommendation, error}){
    if(error){
        return(
            <div className="alert alert-danger text-center">
                <i className="bi bi-exclamation-triangle-fill me-2"></i>
                {error}
            </div>
        );
    }


    if(!recommendation){
        return(
            <div className="text-center py-5">
                <div className="card border-0 bg-light">
                    <div className="card-body py-5">
                        <i className="bi bi-film fs-1 text-muted mb-3"></i>
                        <h3 className="text-muted"> Henüz Film Seçmediniz</h3>
                        <p className="text-muted"> Yukarıdaki butonlardan birini seçerek ve ya özel ruh halinizi yazarak size uygun film önerisi alabilirsiniz</p>
                    </div>
                </div>
            </div>
        );
    }
    //sonrasında bize bir film onerisi dondurcek

    return(
        <div className="recommendation-result mt-5">
            <div className="card shadow-lg border-0">
                <div className="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                    <h3 className="card-title mb-0"> Sizin İçin Önerimiz</h3>
                    <span className='badge bg-light text-dark'>
                        <i className="bi bi-emoji-smile me-1"></i>
                        Ruh Halinize Uygun
                    </span>
                </div>
                <div className="card-body p-4">
                    <div className="row">
                        <div className="col-md-8">
                            <h2 className="mb-3">{recommendation.title}</h2>
                            <div className="d-flex align-items-center mb-4">
                                <div className="badge bg-info text-dark fs-6 me-2">{recommendation.genre}</div>
                                <div className="rating-badge">
                                    <i className="bi bi-star-fill text-warning"></i>
                                    <i className="bi bi-star-fill text-warning"></i>
                                    <i className="bi bi-star-fill text-warning"></i>
                                    <i className="bi bi-star-fill text-warning"></i>
                                    <i className="bi bi-star-half text-warning"></i>
                                    <span className="ms-1">4.5</span>
                                </div>
                        </div>
                        <p className="card-text fs-5"> {recommendation.description || recommendation.reason}</p>

                        <div className="mt-4">
                            <h5> Filmin Öne Çıkan Özellikleri:</h5>
                            <ul className="film-features">
                                <li> <i className="bi bi-check-circle-fill text-success me-2"></i> Ruh hlinize uygun içerik</li>
                                <li> <i className="bi bi-check-circle-fill text-success me-2"></i> Yüksel izleyici puanı</li>
                                <li> <i className="bi bi-check-circle-fill text-success me-2"></i> Kritiklerce beğenilen film</li>
                            </ul>
                        </div>
                    </div>
                    <div className="col-md-4 d-flex align-items-center justify-content-center">
                        <div className="film-poster-placeholder bg-secondary text-white d-flex flex-column align-items-center
                        justify-content-center rounded p-3"
                             style={{width: '100%', maxWidth: '250px', height: '350px' }}>
                          <i className="bi bi-camera-reels fs-1 mb-3"></i>   
                          <p className="text-center">{recommendation.title} Poster </p>             
                        </div>
                    </div>
                </div>
            </div>
            <div className="card-footer bg-light d-flex justify-content-between align-items-center">
                <button className="btn btn-outline-primary" onClick={() => window.location.reload()}>
                    <i className="bi bi-arrow-repeat me-2"></i> Yeni Tavsiye Al
                </button>

            <div className="social-share">
                <button className="btn btn-sm btn-outline-secondary me-2">
                    <i className="bi bi-share me-1"></i> Paylaş
                </button>
                <button className="btn btn-sm btn-outline-danger">
                    <i className="bi bi-heart me-1"></i> Favorilere Ekle
                </button>
              </div>
             </div>
         </div>
       </div>  
    );
}
                //burda sonun da bir tane kapalı div olabilir
export default RecommendationResult;