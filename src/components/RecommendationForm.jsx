import React from "react";

//mod secildigi zaman falan
function RecommendationForm({ onMoodSelect, customMood, setCustomMood, onCustomMoodSubmit }) {
  const moods = [
    { name: 'mutlu', color: 'success', icon: 'bi bi-emoji-smile' },   // bootstrap icon
    { name: 'üzgün', color: 'primary', icon: 'bi bi-emoji-frown' },
    { name: 'öfkeli', color: 'danger', icon: 'bi bi-emoji-angry' },
    { name: 'eğlenceli', color: 'warning', icon: 'bi bi-emoji-laughing' },
    { name: 'random', color: 'secondary', icon: 'bi bi-shuffle' }
  ];

  return (
    <div className="recommendation-form mb-5">
      <div className="text-center mb-4">
        <h2 className="mb-3">Bugün Nasıl Hissediyorsun?</h2>
        <p className="text-muted">Hissettiğin moda göre bir film önerisi al</p>
      </div>

      <div className="d-flex justify-content-center flex-wrap gap-3 mb-4">
        {moods.map((mood, index) => (
          <button
            key={index}
            className={`btn btn-${mood.color} btn-lg d-flex flex-column align-items-center p-4`}
            onClick={() => onMoodSelect(mood.name)}
            style={{ minWidth: '140px', borderRadius: '15px' }}
          >
            <i className={`${mood.icon} fs-2 mb-2`}></i>
            <span>{mood.name.charAt(0).toUpperCase() + mood.name.slice(1)}</span>
          </button>
        ))}
      </div>

      <div className="custom-mood-section bg-light p-4 rounded shadow-sm">
        <h4 className="text-center mb-3">Özel Ruh Hali</h4>
        <p className="text-center mb-3">Kendinize özel bir ruh hali tanımlayın</p>

        <form onSubmit={onCustomMoodSubmit} className="custom-mood-form">
          <div className="row g-2 justify-content-center mb-4">
            <div className="col-md-8 col-lg-6">
              <div className="input-group">
                <input
                  type="text"
                  className="form-control py-2"
                  placeholder="Ruh halinizi yazın (örn: romantik, maceracı)"
                  value={customMood}
                  onChange={(e) => setCustomMood(e.target.value)}
                />
                <button
                  className="btn btn-primary d-flex align-items-center"
                  type="submit"
                >
                  <i className="bi bi-search me-2"></i>
                  <span className="d-none d-md-inline">Film ara</span>
                </button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}

export default RecommendationForm;
