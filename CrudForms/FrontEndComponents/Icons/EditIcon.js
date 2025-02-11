// src/components/icons/EditIcon.js

import React from 'react';

const EditIcon = ({ size = 24, color = "#333333" }) => (
  <svg width={size} height={size} viewBox="0 0 35 35" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path 
      d="M30.625 18.9583V29.1667C30.625 29.9721 29.9721 30.625 29.1667 30.625H5.83333C5.02792 30.625 4.375 29.9721 4.375 29.1667V5.83333C4.375 5.02792 5.02792 4.375 5.83333 4.375H16.0417" 
      stroke={color} 
      strokeWidth="2" 
      strokeLinecap="round" 
      strokeLinejoin="round"
    />
    <path 
      d="M10.2083 19.4833V24.7917H15.5438L30.625 9.70382L25.2985 4.375L10.2083 19.4833Z" 
      stroke={color} 
      strokeWidth="2" 
      strokeLinejoin="round"
    />
  </svg>
);

export default EditIcon;
