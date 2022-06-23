import React from 'react';
import { Position } from '../../types/position';
export interface ToolTipProps {
  children: React.ReactElement;
  message: string | React.ReactNode;
  position?: Position;
}

const ToolTip: React.FC<ToolTipProps> = ({ children, message, position = 'top' }) => {
  let className = '';
  switch (position) {
    case 'top':
      className = '-top-8 before:top-full before:border-t-sky-700';
      break;
    case 'right':
      className = `top-1/2 left-[calc(100%_+_theme('spacing.8'))]
      translate-x-0 -translate-y-1/2
      before:top-1/2 before:-left-1
      before:translate-x-0 before:-translate-y-1/2
      before:border-r-sky-300/60`;
      break;
    case 'bottom':
      className = '-bottom-8 before:bottom-full before:border-b-sky-700';
      break;
    case 'left':
      className = `top-1/2 right-0
      translate-x-0 -translate-y-1/2 left-auto
      before:top-1/2 before:-right-2 before:left-auto
      before:translate-x-0 before:-translate-y-1/2  
      before:border-l-sky-300/60`;
      break;
  }

  return (
    <div className="group relative inline-block">
      {children}
      <span
        className={`absolute left-1/2 -translate-x-1/2 p-1 z-50 
      rounded-lg bg-sky-700 text-sm leading-1 whitespace-nowrap
      invisible group-hover:visible
      before:left-1/2 
      before:border-solid before:border-transparent before:border-4
      before:absolute before:-ml-1 before:h-0 before:w-0 text-white
      ${className}`}
      >
        {message}
      </span>
    </div>
  );
};
export default ToolTip;
