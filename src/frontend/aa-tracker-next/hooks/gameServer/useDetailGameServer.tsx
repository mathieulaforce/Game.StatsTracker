import { useState, useEffect } from 'react';
import axios from 'axios';
import { GameSession } from '../../types/temp';
const useGameSession = (ip: string | null): [boolean, GameSession | undefined] => {
  const [isLoading, setIsLoading] = useState(false);
  const [gameSession, setGameSession] = useState<GameSession | undefined>();

  useEffect(() => {
    const loadServer = async () => {
      try {
        setIsLoading(true);
        const result = await axios.get(' http://localhost:7071/api/Servers/' + ip);
        return result.data;
      } catch (e) {
        console.error(e);
        return [];
      }
    };

    if (!ip) {
      return;
    }

    loadServer().then((result) => {
      setGameSession(result);
      setIsLoading(false);
    });
  }, [ip]);

  return [isLoading, gameSession];
};

export default useGameSession;
