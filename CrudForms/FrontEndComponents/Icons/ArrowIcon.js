// src/components/icons/ArrowIcon.js

import React from 'react';

const ArrowIcon = ({ size = 21, color = "#1B4332" }) => (
  <svg 
    width={size} 
    height={size} 
    viewBox="0 0 34 34" 
    xmlns="http://www.w3.org/2000/svg" 
  >
    <path 
      d="M23.6,16.9l-9.3,9.4c-1,1-2.6,1-3.6,0l0,0c-1-1-1-2.6,0-3.6l5.7-5.8c0.5-0.5,0.5-1.2,0-1.7l-5.7-5.8
        c-1-1-1-2.6,0-3.6l0,0c1-1,2.6-1,3.6,0l9.3,9.4C24.1,15.6,24.1,16.4,23.6,16.9z" 
      fill={color}
      strokeWidth="2" 
      strokeLinecap="round" 
      strokeLinejoin="round" 
    />
  </svg>
);

export default ArrowIcon;
