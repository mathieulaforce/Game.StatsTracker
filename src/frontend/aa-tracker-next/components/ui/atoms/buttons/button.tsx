import React from 'react';
interface ButtonProps {
  onClick?: React.MouseEventHandler<HTMLButtonElement> | null;
  variant?: 'primary' | 'light' | 'dark';
  className?: string;
  children: React.ReactNode;
  disabled?: boolean;
}

export const Button: React.FC<ButtonProps> = ({ onClick, variant = 'light', children, className, disabled }) => {
  let color = 'text-gray-900 hover:bg-gray-300 bg-white hover:text-gray-700 focus-visible:ring-white focus-visible:ring-offset-gray-900 disabled:bg-white/50';
  if (variant === 'dark') {
    color = 'bg-gray-800 text-gray-300 hover:bg-gray-700 focus-visible:ring-gray-700 focus-visible:ring-offset-gray-900 disabled:bg-gray-800/50';
  } else if (variant === 'primary') {
    color = 'focus:ring-slate-400 focus:ring-offset-slate-50 text-white bg-sky-700 highlight-white/20 hover:bg-sky-400 disabled:bg-sky-500/50';
  }

  return (
    <button
      onClick={onClick ?? undefined}
      disabled={disabled}
      className={`rounded-lg p-1 disabled:cursor-not-allowed focus:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 ${color} ${className}`}
    >
      {children}
    </button>
  );
};

export default Button;
