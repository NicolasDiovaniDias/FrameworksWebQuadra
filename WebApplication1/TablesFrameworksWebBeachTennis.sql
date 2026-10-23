--TABLE de Usuários (Clientes, Funcionários, Admins)
CREATE TABLE Usuarios (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    SenhaHash VARCHAR(255) NOT NULL,
    Telefone VARCHAR(20),
    TipoPerfil INTEGER NOT NULL, -- 1: Cliente, 2: Funcionario, 3: Admin
    DataCadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Quadras (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Nome VARCHAR(50) NOT NULL,
    TipoPiso VARCHAR(30),
    Coberta BOOLEAN DEFAULT FALSE,
    ValorHora DECIMAL(10,2) NOT NULL,
    Status INTEGER NOT NULL -- 1: Ativa, 2: Manutenção, 3: Inativa
);

CREATE TABLE Reservas (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UsuarioId UUID NOT NULL,
    QuadraId UUID NOT NULL,
    DataHoraInicio TIMESTAMP NOT NULL,
    DataHoraFim TIMESTAMP NOT NULL,
    ValorTotal DECIMAL(10,2) NOT NULL,
    Status INTEGER NOT NULL, -- 1: Pendente, 2: Confirmada, 3: Cancelada
    
    -- Chaves Estrangeiras (Relacionamentos)
    CONSTRAINT FK_Reservas_Usuarios FOREIGN KEY (UsuarioId) 
        REFERENCES Usuarios (Id) ON DELETE RESTRICT,
    CONSTRAINT FK_Reservas_Quadras FOREIGN KEY (QuadraId) 
        REFERENCES Quadras (Id) ON DELETE RESTRICT
);

CREATE TABLE Pagamentos (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    ReservaId UUID NOT NULL,
    Metodo INTEGER NOT NULL, -- 1: PIX, 2: Cartão de Crédito, 3: Dinheiro
    ValorPago DECIMAL(10,2) NOT NULL,
    DataPagamento TIMESTAMP,
    Status INTEGER NOT NULL, -- 1: Aguardando, 2: Pago, 3: Estornado
    
    -- Chave Estrangeira
    CONSTRAINT FK_Pagamentos_Reservas FOREIGN KEY (ReservaId) 
        REFERENCES Reservas (Id) ON DELETE CASCADE
);

-- Índices para melhorar a performance de consultas comuns
CREATE INDEX IX_Reservas_DataHoraInicio ON Reservas (DataHoraInicio);
CREATE INDEX IX_Reservas_UsuarioId ON Reservas (UsuarioId);
CREATE INDEX IX_Reservas_QuadraId ON Reservas (QuadraId);