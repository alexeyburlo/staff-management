CREATE TABLE public.employee_positions (
    "EmployeeId" uuid NOT NULL,
    "PositionId" uuid NOT NULL
);

ALTER TABLE ONLY public.employee_positions
    ADD CONSTRAINT employee_positions_pkey PRIMARY KEY ("EmployeeId", "PositionId");
    
ALTER TABLE ONLY public.employee_positions
    ADD CONSTRAINT "employee_positions_EmployeeId_fkey" FOREIGN KEY ("EmployeeId") REFERENCES public.employees("Id") ON UPDATE RESTRICT ON DELETE CASCADE;

ALTER TABLE ONLY public.employee_positions
    ADD CONSTRAINT "employee_positions_PositionId_fkey" FOREIGN KEY ("PositionId") REFERENCES public.positions("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;

CREATE TABLE public.employees (
    "Id" uuid NOT NULL,
    "Surname" character varying(255) NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Patronymic" character varying(255) NOT NULL,
    "BirthDate" date NOT NULL,
    "ConcurrencyStamp" character varying NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    "UpdatedAt" timestamp with time zone DEFAULT now() NOT NULL
);

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY ("Id");

CREATE TABLE public.positions (
    "Id" uuid NOT NULL,
    "Title" character varying(255) NOT NULL,
    "Grade" smallint NOT NULL,
    "ConcurrencyStamp" character varying NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    "UpdatedAt" timestamp with time zone DEFAULT now() NOT NULL
);


ALTER TABLE ONLY public.positions
    ADD CONSTRAINT positions_pkey PRIMARY KEY ("Id");
