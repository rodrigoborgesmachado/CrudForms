import React from 'react';

const UserIcon = ({ size = 32, color = '#1B4332' }) => (
    <svg
        width={size}
        height={size}
        viewBox="0 0 21 21"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
    >
        {/* Head */}
        <circle cx="12" cy="8" r="4" fill="none" stroke={color} strokeWidth="2" />
        {/* Body */}
        <path
            d="M4 19C4 15.6863 7.13401 13 12 13C16.866 13 20 15.6863 20 19"
            stroke={color}
            strokeWidth="2"
            fill="none"
        />
    </svg>
);

export default UserIcon;
