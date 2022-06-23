import * as React from 'react';

interface PanelProps {
  children?: React.ReactNode;
  className?: string;
}

const Panel: React.FC<PanelProps> = ({ children, className }) => {
  return <div className={`p-2 bg-slate-800 rounded shadow-lg ring-1 ring-white/10 ring-inset text-white/80 m1 ${className}`}>{children}</div>;
};
export default Panel;
