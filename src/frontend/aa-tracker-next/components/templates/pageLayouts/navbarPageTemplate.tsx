import * as React from 'react';
import Navbar from '../../ui/organism/navbar/navbar';

const NavbarPageTemplate: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div>
      <Navbar />
      <main className="p-4">{children}</main>
    </div>
  );
};

export default NavbarPageTemplate;
