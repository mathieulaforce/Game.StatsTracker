import React, { useRef, useEffect } from 'react';

const useClickOutsideElement = (ref: React.MutableRefObject<any>, onOutsideClick: () => void) => {
  useEffect(() => {
    const clickedOutsideElement = (event: any) => {
      if (ref.current && !ref.current.contains(event.target)) {
        onOutsideClick();
      }
    };

    if (ref !== null) {
      document.addEventListener('mousedown', clickedOutsideElement);
      return () => {
        document.removeEventListener('mousedown', clickedOutsideElement);
      };
    }
  }, [ref, onOutsideClick]);
};

interface ClickOutsideElementHandlerProps {
  children: React.ReactNode;
  onOutsideClick: () => void;
}

const ClickOutsideElementProvider: React.FC<ClickOutsideElementHandlerProps> = (props) => {
  const wrapperRef = useRef(null);
  useClickOutsideElement(wrapperRef, props.onOutsideClick);

  return <div ref={wrapperRef}>{props.children}</div>;
};

export default ClickOutsideElementProvider;
