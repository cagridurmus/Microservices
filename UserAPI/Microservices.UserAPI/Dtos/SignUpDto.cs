﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Microservices.UserAPI.Dtos
{
	public class SignUpDto
	{
		public string Email { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }
	}
}

